// /// <reference path="player.ts" />
// /// <reference path="game.ts" />

import {Player} from './player';
import {Game} from './game';
import * as Helpers from './utility';	//altro modo di fare import, con * importo tutto il modulo esportato da utility, e metto anche un alias


let newGame: Game;

document.getElementById('startGame')!.addEventListener('click', () => {
	const player: Player = new Player();
	player.name = Helpers.getValue('playername');
	
	const problemCount: number = Number(Helpers.getValue('problemCount'));
	const factor: number = Number(Helpers.getValue('problemCount'));

	newGame = new Game(player, problemCount, factor);
	newGame.displayGame();
});


document.getElementById('calculate')!.addEventListener('click', () => {
	newGame.calculateScore();

});

