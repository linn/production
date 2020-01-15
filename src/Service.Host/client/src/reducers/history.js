const initialState = [];

function history(state = initialState, action) {
    if (typeof state === 'undefined') {
        return state;
    }

    if (action.type === '@@router/LOCATION_CHANGE') {
        return [...state, action.payload.prevPathname];
    }
    return state;
}

export default history;
