import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { labelPrinterActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.labelPrinters.item,
    itemTypes.labelPrinters.actionType,
    itemTypes.labelPrinters.uri,
    actionTypes,
    config.appRoot
);
