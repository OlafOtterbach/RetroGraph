let id = 1;
let bodyId;
let lock = false;
let currentCamera;
let mouseMoved = false;
let currentPosition;
let lineWidth =  1.0;
let backgroundColor = "#FFFFFF";
let foregroundColor = "#000000"

let canvas = document.querySelector("canvas");
canvas.addEventListener("mousedown", onMouseDown);
canvas.addEventListener("mouseup", onMouseUp);
var ctx = canvas.getContext("2d");
getScenery();

function Position(x, y) {
    this.x = x;
    this.y = y;
}

function MoveStateDto() {
    bodyId = "00000000-0000-0000-0000-000000000000";
    startX = 0.0;
    startY = 0.0;
    endX = 0.0;
    endY = 0.0;
    canvasWidth = 0;
    canvasHeight = 0;
    camera = null;
}

function SelectStateDto() {
    selectPositionX = 0.0;
    selectPositionY = 0.0;
    canvasWidth = 0;
    canvasHeight = 0;
    camera = null;
}

function ZoomStateDto() {
    delta = 0.0;
    canvasWidth = 0;
    canvasHeight = 0;
    camera = null;
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

function onMouseDown(event) {
    if (event.button === 0 || event.button === 2) {
        canvas.addEventListener("mousemove", onMouseMoved);
        canvas.addEventListener("contextmenu", onContextMenu);

        mouseMoved = false;
        currentPosition = getPosition(event, canvas)
        selectBody(currentPosition.x, currentPosition.y, currentCamera);
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
            if (event.buttons === 1) {
                move(bodyId, currentPosition, movedPosition, currentCamera);
            } else {
                zoom(currentPosition, movedPosition, currentCamera);
            }
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
    lock = true;
    let url = encodeURI("http://localhost:5000/initial-graphics?canvasWidth=" + canvas.width + "&canvasHeight=" + canvas.height);
    let graphics = await fetchData(url);
    lock = false;
    drawScene(graphics);
}

async function selectBody(x, y, camera) {
    lock = true;
    camera.Id = id++;
    let selectState = new SelectStateDto();
    selectState.camera = camera;
    selectState.selectPositionX = x;
    selectState.selectPositionY = y;
    selectState.canvasWidth = canvas.width;
    selectState.canvasHeight = canvas.height;
    let url = encodeURI("http://localhost:5000/select-body");
    let bodySelection = await postData(url, selectState);
    bodyId = bodySelection.BodyId;
    lock = false;
}

async function select(x, y, camera) {
    lock = true;
    camera.Id = id++;
    let selectState = new SelectStateDto();
    selectState.camera = camera;
    selectState.selectPositionX = x;
    selectState.selectPositionY = y;
    selectState.canvasWidth = canvas.width;
    selectState.canvasHeight = canvas.height;
    let url = encodeURI("http://localhost:5000/select");
    let graphics = await postData(url, selectState);
    lock = false;
    drawScene(graphics);
}

async function move(bodyId, start, end, camera) {
    sleep(50);
    if (!lock) {
        lock = true;
        camera.Id = id++;
        let moveState = new MoveStateDto();
        moveState.camera = camera;
        moveState.bodyId = bodyId;
        moveState.startX = start.x;
        moveState.startY = start.y;
        moveState.endX = end.x;
        moveState.endY = end.y;
        moveState.canvasWidth = canvas.width;
        moveState.canvasHeight = canvas.height;
        let url = encodeURI("http://localhost:5000/move");
        let graphics = await postData(url, moveState);
        drawScene(graphics);
        currentPosition = end;
        lock = false;
    }
}

async function zoom(start, end, camera) {
    sleep(50);
    if (!lock) {
        lock = true;
        camera.Id = id++;
        let delta = end.y - start.y;
        let zoomState = new ZoomStateDto();
        zoomState.camera = camera;
        zoomState.delta = delta;
        zoomState.canvasWidth = canvas.width;
        zoomState.canvasHeight = canvas.height;
        let url = encodeURI("http://localhost:5000/zoom");
        let graphics = await postData(url, zoomState);
        drawScene(graphics);
        currentPosition = end;
        lock = false;
    }
}

function drawScene(graphics) {
    if (graphics !== undefined) {
        currentCamera = graphics.Camera;

        ctx.beginPath();
        ctx.setLineDash([]);
        ctx.fillStyle = backgroundColor;
        ctx.fillRect(0, 0, canvas.width, canvas.height);
        ctx.strokeStyle = foregroundColor;
        ctx.lineWidth = lineWidth;
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
        ctx.closePath();
        ctx.stroke();
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