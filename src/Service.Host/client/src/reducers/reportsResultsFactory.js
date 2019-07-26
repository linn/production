// Had to add this to cope with an array of reports. Will add to shared library if useful.
export default function(itemRoot, actionTypes, defaultState = { loading: false, data: null }) {
    return (state = defaultState, action) => {
        switch (action.type) {
            case actionTypes[`REQUEST_${itemRoot}_REPORT`]:
                return {
                    ...state,
                    loading: true,
                    data: null
                };

            case actionTypes[`RECEIVE_${itemRoot}_REPORT`]:
                return {
                    ...state,
                    loading: false,
                    data: action.payload.data
                };

            default:
                return state;
        }
    };
}
