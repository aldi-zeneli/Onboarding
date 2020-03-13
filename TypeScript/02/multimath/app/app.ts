function startGame(){
	//Commento che verrà eliminato dopo compilazione
	
	//es di uso di variabili let e const al posto di var + annotation
	//const playerName: string = 'aldi';
	let playerName: string = 'aldi';
	 logPlayer(playerName);	

	//es di Union Type, permettono quando dichiaro una var di esprimere gli unici tipo di val che accetta
	//let nameOrAge: string | number;
	//let name : null | undefined | string;

	//es di Type Assertion, da unsare se ho var di tipo non definito, ma di cui per qualche motivo so che tipo avrà (es: funz che ritorna any)
	//let value: any = 5;
	//let a: string = (<number>value).toFixed(4);
	//let a: string = (value as number).toFixed(4); 	//sintassi alternativa
	//console.log(a);


	//ES di Control Flow Type Analysis, che restringe il tipo di una var a seconda del flusso del codice
	// var messagesElement: HTMLElement | string;	//dichiaro var con 2 tipi ammessi

	// if(typeof messagesElement == 'string'){	//in questo if esntra sse è una stringa => la var è considerata string (vacci sopra col mouse)
	// 	return messagesElement;
	// }
	// else{	//qui entra sse non è string => è HTMLEelement
	// 	return messagesElement;		
	// }
	//qui sotto può essere entrambi i tipoi =>  è una union



	var messagesElement = document.getElementById('messages');
	messagesElement!.innerText = 'Welcome to multimath! starting new game..'
}

function logPlayer(name){
	console.log('New game starting for player: ', name);
}

document.getElementById('startGame')!.addEventListener('click', startGame);

