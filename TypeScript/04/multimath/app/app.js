var Player = (function () {
    function Player() {
    }
    Player.prototype.formatName = function () {
        return this.name.toUpperCase();
    };
    return Player;
}());
function startGame() {
    var playerName = getInputValue('playername');
    logPlayer(playerName);
    postScore(100, playerName);
    postScore(-6, playerName);
}
function logPlayer(name) {
    if (name === void 0) { name = 'Aldi'; }
    console.log('New game starting for player: ', name);
}
function getInputValue(elementID) {
    var inputElement = document.getElementById(elementID);
    if (inputElement.value === '') {
        return undefined;
    }
    else {
        return inputElement.value;
    }
}
function postScore(score, playerName) {
    if (playerName === void 0) { playerName = 'Aldi'; }
    var logger;
    if (score < 0) {
        logger = logError;
    }
    else {
        logger = logMessage;
    }
    var scoreElement = document.getElementById('postedScores');
    var scoreText = score + '  -  ' + playerName;
    scoreElement.innerText = scoreText;
    logger("Score: " + score);
}
var logMessage = function (message) { return console.log(message); };
function logError(err) {
    console.error(err);
}
document.getElementById('startGame').addEventListener('click', startGame);
var firstPlayer = new Player();
firstPlayer.name = 'Player1';
console.log(firstPlayer.formatName());
//# sourceMappingURL=app.js.map