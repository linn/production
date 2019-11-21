import { save, WORKS_ORDER_PRINTER_KEY } from '../helpers/localStorage';
import * as actionTypes from '../actions';

const localStorageMiddleWare = () => next => action => {
    const result = next(action);

    if (action.type === actionTypes.SET_DEFAULT_WORKS_ORDER_PRINTER) {
        save(WORKS_ORDER_PRINTER_KEY, action.payload);
    }

    return result;
};

export default localStorageMiddleWare;
