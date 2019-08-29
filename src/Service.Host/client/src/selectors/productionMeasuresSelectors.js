export const getLoading = state => {
    if (!state.productionMeasures) {
        return null;
    }

    if (!state.productionMeasures.info) {
        return null;
    }

    if (!state.productionMeasures.cits) {
        return null;
    }

    return state.productionMeasures.cits.loading && state.productionMeasures.info.loading;
};

export const getCitsData = state => {
    if (!state.productionMeasures) {
        return null;
    }

    if (!state.productionMeasures.cits) {
        return null;
    }

    return state.productionMeasures.cits.data;
};

export const getInfoData = state => {
    if (!state.productionMeasures) {
        return null;
    }

    if (!state.productionMeasures.info) {
        return null;
    }

    return state.productionMeasures.info.data;
};
