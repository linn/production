import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { labelPrintTypeActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.labelPrintTypes.item,
    itemTypes.labelPrintTypes.actionType,
    itemTypes.labelPrintTypes.uri,
    actionTypes,
    config.appRoot
);
