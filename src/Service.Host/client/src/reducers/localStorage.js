import { SELECT_DEFAULT_WORKS_ORDER_PRINTER, SET_DEFAULT_WORKS_ORDER_PRINTER } from '../actions';
import * as itemTypes from '../itemTypes';

const defaultState = [
    {
        item: itemTypes.worksOrder.item,
        defaultPrinter: ''
    }
];

const localStorage = (state = defaultState, action) => {
    switch (action.type) {
        case SELECT_DEFAULT_WORKS_ORDER_PRINTER:
        case SET_DEFAULT_WORKS_ORDER_PRINTER: {
            const items = state.filter(item => item.item !== itemTypes.worksOrder.item);
            const newItem = action.payload;

            return [...items, newItem];
        }

        default:
            return state;
    }
};

export default localStorage;
