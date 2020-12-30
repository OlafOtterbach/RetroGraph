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
    this.bodyId = "00000000-0000-0000-0000-000000000000";
    this.startX = 0.0;
    this.startY = 0.0;
    this.endX = 0.0;
    this.endY = 0.0;
    this.canvasWidth = 0;
    this.canvasHeight = 0;
    this.camera = null;
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
    let url = encodeURI("http://localhost:5000/select-body?canvasX=" + x + "&canvasY=" + y + "&canvasWidth=" + canvas.width + "&canvasHeight=" + canvas.height);
    let bodySelection = await postData(url, camera);
    console.log(bodySelection.BodyId);
    bodyId = bodySelection.BodyId;
    lock = false;
}

async function select(x, y, camera) {
    lock = true;
    camera.Id = id++;
    let url = encodeURI("http://localhost:5000/select?canvasX=" + x + "&canvasY=" + y + "&canvasWidth=" + canvas.width + "&canvasHeight=" + canvas.height);
    let graphics = await postData(url, camera);
    lock = false;
    console.log(graphics.Camera.Id);
    drawScene(graphics);
}

async function move(bodyId, start, end, camera) {
    sleep(50);
    if (!lock) {
        lock = true;

        var moveState = new MoveStateDto();
        camera.Id = id++;
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
        let url = encodeURI("http://localhost:5000/zoom?delta=" + delta + "&canvasWidth=" + canvas.width + "&canvasHeight=" + canvas.height);
        let graphics = await postData(url, camera);
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