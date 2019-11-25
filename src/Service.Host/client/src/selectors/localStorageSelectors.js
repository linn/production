const getWorksOrderDefaultPrinter = (state, itemType) =>
    state.localStorage.find(item => item.item === itemType)?.defaultPrinter;

export default getWorksOrderDefaultPrinter;
