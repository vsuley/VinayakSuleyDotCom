(function ($) {

  // State and Data Model
  var _photoCollection = null;
  var _fullMode = false;

  // IDs, classes and attributes.
  var _panelSliderId = '#panelSlider';
  var _slideshowContainerId = '#slideshowContainer';
  var _fullImageClickTargetId = "#fullImageClickTarget";
  var _fullModeHelpTextId = "#fullModeHelpText";
  var _fullImageId = '#fullImage';
  var _topAlign1Class = '.topAlign1';
  var _displayCellClass = '.displayCell';
  var _indexAttribute = "index";

  // Slider calculations
  var _sliderMinLeft = 'uninitialized';
  var _sliderMaxLeft = 'uninitialized';
  var _spaceForHeader = 100;
  var _spaceForFooter = 25;
  var _shiftAmount = 300;
  var _sliderHeight = 500;
  var _intuitionGutter = 160;

  // Full image calculations
  var _maxFullEdge = 640;

  // Animations and timing
  var _animationDuration1 = 200;  // Used in: Large shifts in slider.

  // The ONE event handler that we want to attach outside of functions:
  $(document).bind('ready', onReady);

  // Event handler implementations.
  function keydownHandler(e) {

    if (!_fullMode) {

      sliderKeyboardHandler(e);
    }
    else {

      fullModeKeyboardHandler(e);
    }
  }

  function sliderKeyboardHandler(e) {

    // Left cursor key.
    if (e.keyCode == 37) {
      shiftSliderRight(_shiftAmount);
    }

    // Right cursor key
    else if (e.keyCode == 39) {
      shiftSliderLeft(_shiftAmount);
    }
  }

  function fullModeKeyboardHandler(e) {

    var photoIndex = $(_fullImageId).attr(_indexAttribute);

    // Left cursor key. previous image
    if (e.keyCode == 37) {

      if (photoIndex == 0) {
        return;
      }
      photoIndex--;
    }

    // Right cursor key. next image.
    else if (e.keyCode == 39) {

      if (photoIndex == _photoCollection.length - 1) {

        return;
      }
      photoIndex++;
    }

    // Escape. Quit full mode.
    else if (e.keyCode == 27) {

      toggleFullMode();
    }

    showFullImage(photoIndex);
  }

  function startContinuedScroll(e) {

    var duration, finalDestination;
    var sliderLeft = $(_panelSliderId).offset().left;

    if (e.data.Direction > 0) { // slide right

      finalDestination = _sliderMaxLeft;
    }
    else { // slide left
      finalDestination = _sliderMinLeft;
    }

    duration = Math.abs(finalDestination - sliderLeft) * e.data.millisecondsPerPixel;
    $(_panelSliderId).animate({ left: finalDestination + 'px' }, duration);
  }

  function endContinuedScroll() {

    $(_panelSliderId).stop();
  }

  function shiftSliderLeft(shiftAmount) {

    var sliderLeft = $(_panelSliderId).offset().left;

    // There's no space left
    // This is being treated as a special case because we might want to 
    // have a special bump animation later
    if (sliderLeft <= _sliderMinLeft) {
      sliderLeft = _sliderMinLeft;
    }

    // There's some space left, but less than all of the standard animation to take place.
    else if (sliderLeft - shiftAmount <= _sliderMinLeft) {
      sliderLeft = _sliderMinLeft;
    }

    // Standard case.
    else {
      sliderLeft -= shiftAmount;
    }

    $(_panelSliderId).stop(); // To remove any ongoing animations
    $(_panelSliderId).animate({ left: sliderLeft + 'px' }, _animationDuration1);
  }

  function shiftSliderRight(shiftAmount) {

    var sliderLeft = $(_panelSliderId).offset().left;

    // There's no space left
    // This is being treated as a special case because we might want to 
    // have a special bump animation later
    if (sliderLeft >= _sliderMaxLeft) {
      sliderLeft = _sliderMaxLeft;
    }

    // There's some space left, but less than all of the standard animation to take place.
    else if (sliderLeft + shiftAmount >= _sliderMaxLeft) {
      sliderLeft = _sliderMaxLeft;
    }

    // Standard case.
    else {
      sliderLeft += shiftAmount;
    }

    $(_panelSliderId).stop();
    $(_panelSliderId).animate({ left: sliderLeft + 'px' }, _animationDuration1);
  }

  function onReady(e) {

    // Grab data first.
    _photoCollection = $.parseJSON(window.photoCollectionJSON);

    // Construct any remaining DOM first.

    // Attach any remaining event handlers etc.
    attachEventHandlersToElements(e);

    // Finish layout
    setupBasicLayout(e);
  }

  function attachEventHandlersToElements(e) {

    // Window events.
    $(document).bind('keydown', keydownHandler);
    $(window).bind('resize', windowResize);

    // Slider mode scroll zones.

    $('#leftScrollZone').bind('mouseenter', { Direction: 1, millisecondsPerPixel: 1 }, startContinuedScroll);
    $('#rightScrollZone').bind('mouseenter', { Direction: -1, millisecondsPerPixel: 1 }, startContinuedScroll);
    $('#leftScrollZone').bind('mouseleave', endContinuedScroll);
    $('#rightScrollZone').bind('mouseleave', endContinuedScroll);

//    $('#leftScrollZone').bind('mouseover', { Direction: 1, millisecondsPerPixel: 1 }, startContinuedScroll);
//    $('#rightScrollZone').bind('mouseover', { Direction: -1, millisecondsPerPixel: 1 }, startContinuedScroll);
//    $('#leftScrollZone').bind('mouseout', endContinuedScroll);
//    $('#rightScrollZone').bind('mouseout', endContinuedScroll);

    // Click handlers for all display cells
    $(_displayCellClass).each(function (index) {

      $(this).bind('click', { photoIndex: index }, toggleFullMode);
    });

    // Click handler for full image click area
    $(_fullImageClickTargetId).bind('click', toggleFullMode);
  }

  function setupBasicLayout(e) {

    var divisionFactor = 2.8; // calculated emperically
    var topAlign1Top;

    // Do general sweeps over layout classes
    // 

    // topAlign1
    topAlign1Top = Math.max(($(window).height() - _spaceForHeader - _sliderHeight) / divisionFactor, _spaceForHeader);
    $(_topAlign1Class)
            .offset({ top: topAlign1Top });

    // Now do component-by-component layouts.
    // 

    // SlideshowContainer
    slideshowContainerHeight = $(window).height() - $(_slideshowContainerId).offset().top - _spaceForFooter;
    slideshowContainerHeight = Math.max(slideshowContainerHeight, _sliderHeight + _spaceForFooter);

    $(_slideshowContainerId)
            .height(slideshowContainerHeight);

    // Panel Slider
    $(_panelSliderId)
            .height(_sliderHeight);

    // Now calculate slider limits and initialize
    //
    calculateSliderLimits();
    $(_panelSliderId).offset({ left: _sliderMaxLeft });
  }

  function windowResize(e) {

    setupBasicLayout(null);

    // If we're in fullmode then reposition elements there as well.
    if (_fullMode) {

      // Calling showFullImage will recalculate element postions and sizes.
      var photoIndex = $(_fullImageId).attr(_indexAttribute);
      showFullImage(photoIndex);
    }
  }

  // Helper methods
  function calculateSliderLimits() {

    _sliderMinLeft = 0 - _intuitionGutter - $(_panelSliderId).width() + $(window).width();
    _sliderMaxLeft = _intuitionGutter;
  }

  function toggleFullMode(eventData) {

    // Currently in slider mode...going INTO full mode.
    if (!_fullMode) {

      var photoIndex = $(this).attr(_indexAttribute);
      showFullImage(photoIndex);

      // we're ready, now make the stuff visible.
      $('.toggleHidden').show();

      // Show help text briefly.
      $(_fullModeHelpTextId)
        .fadeIn(500)
        .delay(3000)
        .fadeOut(1000)
        .hide(1);

      // flag that we're in full mode
      _fullMode = true;
    }

    // Currently in Full mode...COMING OUT of full mode
    else {

      $('#fullImage').attr("src", "#");
      $('.toggleHidden').hide();
      _fullMode = false;
    }
  }

  function showFullImage(photoIndex) {

    var imageurl = _photoCollection[photoIndex].PathToMedium;
    var aspect = _photoCollection[photoIndex].Aspect;

    // calculate image size to be shown.
    var imageWidth, imageHeight;
    if (aspect > 1) {
      imageWidth = _maxFullEdge;
      imageHeight = _maxFullEdge / aspect;
    }
    else {
      imageWidth = _maxFullEdge * aspect;
      imageHeight = _maxFullEdge;
    }

    // calculate image position
    var imageTop, imageLeft;
    imageLeft = ($(window).width() - imageWidth) / 2;
    imageTop = ($(window).height() - imageHeight) / 2;

    $(_fullImageId)
      .attr("src", imageurl)
      .attr(_indexAttribute, photoIndex)
      .css("position", "absolute")
      .css("left", imageLeft + 'px')
      .css("top", imageTop + 'px');
  }

})(jQuery);

