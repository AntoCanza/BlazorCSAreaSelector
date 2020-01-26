window.getOffsetLeft = function (elementId) {
    return document.getElementById(elementId).getBoundingClientRect().left;
};

window.getOffsetTop = function (elementId) {
    return document.getElementById(elementId).getBoundingClientRect().top;
};