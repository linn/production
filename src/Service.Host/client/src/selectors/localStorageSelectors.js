const getWorksOrderDefaultPrinter = (state, itemType) => {
    return state.localStorage.find(item => item.item === itemType)?.defaultPrinter;
};

export default getWorksOrderDefaultPrinter;
