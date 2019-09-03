function productionMeasuresCits(state = { loading: false, data: null }, action) {
    switch (action.type) {
        case 'REQUEST_PRODUCTION_MEASURES_CITS_REPORT':
            return {
                ...state,
                loading: true,
                data: null
            };

        case 'RECEIVE_PRODUCTION_MEASURES_CITS_REPORT':
            return {
                ...state,
                loading: false,
                data: action.payload.data
            };

        default:
            return state;
    }
}

export default productionMeasuresCits;
