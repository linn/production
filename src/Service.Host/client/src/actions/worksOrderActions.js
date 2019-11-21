import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import {
    worksOrderActionTypes as actionTypes,
    SELECT_DEFAULT_WORKS_ORDER_PRINTER,
    SET_DEFAULT_WORKS_ORDER_PRINTER
} from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';
import { load, WORKS_ORDER_PRINTER_KEY } from '../helpers/localStorage';

export default new UpdateApiActions(
    itemTypes.worksOrder.item,
    itemTypes.worksOrder.actionType,
    itemTypes.worksOrder.uri,
    actionTypes,
    config.appRoot
);

const getDefaultPrinterForUser = (itemType, defaultPrinter) => ({
    type: SELECT_DEFAULT_WORKS_ORDER_PRINTER,
    payload: { item: itemType, defaultPrinter }
});

export const getDefaultWorksOrderPrinter = itemType => dispatch => {
    const defaultPrinter = load(WORKS_ORDER_PRINTER_KEY);
    dispatch(getDefaultPrinterForUser(itemType, defaultPrinter));
};

export const setDefaultWorksOrderPrinter = (itemType, defaultPrinter) => ({
    type: SET_DEFAULT_WORKS_ORDER_PRINTER,
    payload: { item: itemType, defaultPrinter }
});
