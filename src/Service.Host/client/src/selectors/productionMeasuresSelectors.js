export const getLoading = state => {
    if (!state.productionMeasures) {
        return null;
    }

    return state.productionMeasures.results.loading;
};

export const getCitsData = state => {
    if (!state.productionMeasures) {
        return null;
    }

    return state.productionMeasures.results.data;
};
