
function getInputValue(elementID: string) : string {
    const inputElement: HTMLInputElement = <HTMLInputElement>document.getElementById(elementID);
        return inputElement.value;
    
}

function logger(message: string) : void{
	console.log(message);
}

export {getInputValue as getValue,  logger}; //rendo accessibili le 2 f agli altri moduli, e alla prima f metto un alias
