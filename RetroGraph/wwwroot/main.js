let currentCamera;
let mouseMoved = false;
let currentPosition;
let backgroundColor = "black";
let foregroundColor = "lightgreen"

let canvas = document.querySelector("canvas");
canvas.addEventListener("mousedown", onMouseDown);
canvas.addEventListener("mouseup", onMouseUp);
var ctx = canvas.getContext("2d");
getScenery();

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
    if (event.button === 0) {
        canvas.removeEventListener("mousemove", onMouseMoved);
        canvas.removeEventListener("contextmenu", onContextMenu);

        if (!mouseMoved) {
            currentPosition = getPosition(event, canvas)
            select(currentPosition.x, currentPosition.y, currentCamera);
        }
        mouseMoved = false;
    }
}

function onMouseMoved(event) {
    if (event.buttons === 0) {
        canvas.removeEventListener("mousemove", onMouseMoved);
    } else {
        if (event.buttons === 1 || event.buttons === 2) {
            mouseMoved = true;
            let movedPosition = getPosition(event, canvas)
            let xdelta = movedPosition.x - currentPosition.x;
            let ydelta = movedPosition.y - currentPosition.y;
            if (event.buttons === 1) {
                orbit(xdelta, ydelta, currentCamera);
            } else {
                zoom(ydelta, currentCamera);
            }
            currentPosition = movedPosition;
        }
    }
}

function onContextMenu(event) {
    event.preventDefault();
    return false;
}

function getPosition(event, canvas) {
    let rect = canvas.getBoundingClientRect();
    return new Position(event.clientX - rect.left, event.clientY - rect.top);
}

async function getScenery() {
    let url = encodeURI("http://localhost:5000/initial-graphics?canvasWidth=" + canvas.width + "&canvasHeight=" + canvas.height);
    let graphics = await fetchData(url);
    drawScene(graphics);
}

async function select(x, y, camera) {
    let url = encodeURI("http://localhost:5000/select?canvasX=" + x + "&canvasY=" + y + "&canvasWidth=" + canvas.width + "&canvasHeight=" + canvas.height);
    let graphics = await postData(url, camera);
    drawScene(graphics);
}

async function orbit(deltaX, deltaY, camera) {
    let url = encodeURI("http://localhost:5000/orbit?deltaX=" + deltaX + " &deltaY=" + deltaY + "&canvasWidth=" + canvas.width + "&canvasHeight=" + canvas.height);
    let graphics = await postData(url, camera);
    drawScene(graphics);
}

async function zoom(delta, camera) {
    let url = encodeURI("http://localhost:5000/zoom?delta=" + delta + "&canvasWidth=" + canvas.width + "&canvasHeight=" + canvas.height);
    let graphics = await postData(url, camera);
    drawScene(graphics);
}

function drawScene(graphics) {
    if (graphics !== undefined) {
        currentCamera = graphics.Camera;

        ctx.beginPath();
        ctx.setLineDash([]);
        ctx.fillStyle = backgroundColor;
        ctx.fillRect(0, 0, canvas.width, canvas.height);
        ctx.strokeStyle = foregroundColor;
        ctx.lineWidth = 2;
        ctx.lineCap = "round";
        ctx.setLineDash([]);

        var lines = graphics.DrawLines;
        var n = lines.length;
        for (i = 0; i < n; i += 4) {
            let x1 = lines[i];
            let y1 = lines[i + 1];
            let x2 = lines[i + 2];
            let y2 = lines[i + 3];
            drawLine(x1, y1, x2, y2);
        }
        ctx.stroke();
        ctx.endPath();
    }
}

function drawLine(x1, y1, x2, y2) {
    ctx.moveTo(x1, y1);
    ctx.lineTo(x2, y2);
}

function fetchData(url) {
    let result = fetch(url)
        .then(function (response) {
            if (response.ok)
                return response.json();
            else
                throw new Error('server can has not connected');
        }).catch(function (err) {
            // Error
        });
    return result;
}

function postData(url, data) {
    let result = fetch(url, {
        method: "POST",
        mode: "cors",
        cache: "no-cache",
        credentials: "same-origin",
        headers: {
            "Content-Type": "application/json",
        },
        redirect: "follow",
        referrer: "no-referrer",
        body: JSON.stringify(data),
    }).then(function (response) {
        if (response.ok)
            return response.json();
        else
            throw new Error('server can has not connected');
    }).catch(function (err) {
        // Error
    });
    return result;
}