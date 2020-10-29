let currentCamera = getCamera();
let mouseMoved = false;
let currentPosition;
let canvas = document.querySelector("canvas");
canvas.addEventListener("mousedown", onMouseDown);
canvas.addEventListener("mouseup", onMouseUp);
drawScene();

function Position(x, y) {
    this.x = x;
    this.y = y;
}

function onMouseDown(event) {
    if (event.button === 0 || event.button === 2) {
        canvas.addEventListener("mousemove", onMouseMoved);
        canvas.addEventListener("contextmenu", onContextMenu);

        mouseMoved = false;
        currentPosition = getPosition(event, canvas)
    }
}

function onMouseUp(event) {
    if (event.button === 0 || event.button === 2) {
        canvas.removeEventListener("mousemove", onMouseMoved);
        canvas.removeEventListener("contextmenu", onContextMenu);

        mouseMoved = false;
        currentPosition = getPosition(event, canvas)
        select(currentPosition.x, currentPosition.y, canvas.width, canvas.height, currentCamera);
        drawScene();
    }
}

function onContextMenu(event) {
    event.preventDefault();
    return false;
}



function onMouseMoved(event) {
    if (event.buttons === 0) {
        canvas.removeEventListener("mousemove", onMouseMoved);
    } else {
        if (event.button === 0 || event.button === 2) {
            mouseMoved = true;
            let movedPosition = getPosition(event, canvas)
            let xdelta = movedPosition.x - currentPosition.x;
            let ydelta = movedPosition.y - currentPosition.y;
            if (event.button === 0) {
                currentCamera = orbit(xdelta, ydelta, camera);
            } else {
                currentCamera = zoom(ydelta, camera);
            }

            drawScene();

            let cx = document.querySelector("canvas").getContext("2d");
            cx.beginPath();
            cx.moveTo(currentPosition.x, currentPosition.y);
            cx.lineTo(currentPosition.x + xdelta, currentPosition.y + ydelta);
            cx.stroke();

            currentPosition = movedPosition;
        }
    }
}


function getPosition(event, canvas) {
    let rect = canvas.getBoundingClientRect();
    return new Position(event.clientX - rect.left, event.clientY - rect.top);
}

function drawScene() {

}

function getCamera() {
    return camera;
}

function select(x, y, canvasWidth, canvasHeight, camera) {
    return camera;
}

function orbit(xdelta, ydelta, camera) {
    return camera;
}

function zoom(delta, camera) {
    return camera;
}
