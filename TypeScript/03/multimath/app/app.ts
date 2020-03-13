function startGame(){
	//Commento che verrà eliminato dopo compilazione
	
	let playerName: string | undefined =  getInputValue('playername');
	 logPlayer(playerName);	

	 postScore(100,  playerName);
	 postScore(-6,  playerName);
}

function logPlayer(name: string = 'Aldi') : void{
	console.log('New game starting for player: ', name);
}

function getInputValue(elementID: string) : string | undefined{
	const inputElement: HTMLInputElement = <HTMLInputElement>document.getElementById(elementID);
	
	if(inputElement.value === ''){
		return undefined;
	}
	else{
		return inputElement.value;
	}
}

function postScore(score: number,  playerName: string = 'Aldi') : void{
	
	let logger: (value: string) => void;	//var logger può contenere una qualisasi funzione con quella firma. NOTARE I : INVECE DELL =
	
	//in base al punteggio scelgo qualche funzione assegnare a logger
	if(score < 0){
		logger = logError;
	}
	else{
		logger = logMessage;
	}
	
	const scoreElement: HTMLElement | null = document.getElementById('postedScores');
	let scoreText  = score +'  -  '+ playerName;
	scoreElement!.innerText = scoreText; // '${score} - ${playerName}';

	logger("Score: "+ score);
}

const logMessage = (message: string) => console.log(message);
function logError(err: string)  : void{
	console.error(err);
}


document.getElementById('startGame')!.addEventListener('click', startGame);

