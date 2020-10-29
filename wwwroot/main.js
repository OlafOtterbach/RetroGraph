let xpos;
let ypos;

let canvas = document.querySelector("canvas");

canvas.addEventListener("mousedown", onMousePressed);



function Position(x, y) {
    this.x = x;
    this.y = y;
}

function onMousePressed(event) {
    if (event.button === 0) {
        let pos = pointerPosition(event, canvas)
        xpos = pos.x;
        ypos = pos.y;
        window.addEventListener("mousemove", onMouseMoved);
        event.preventDefault();
    }
}

function onMouseMoved(event) {
    if (event.buttons === 0) {
        window.removeEventListener("mousemove", onMouseMoved);
    } else {
        let pos = pointerPosition(event, canvas)
        let xdelta = pos.x - xpos;
        let ydelta = pos.y - ypos;

        cx.beginPath();
        cx.moveTo(xpos, ypos);
        cx.lineTo(xpos + xdelta, ypos + ydelta);
        cx.stroke();

        xpos = pos.x;
        ypos = pos.y;
    }
}


function pointerPosition(event, canvas) {
    let rect = canvas.getBoundingClientRect();
    return new Position(event.clientX - rect.left, event.clientY - rect.top);
}


let cx = document.querySelector("canvas").getContext("2d");
