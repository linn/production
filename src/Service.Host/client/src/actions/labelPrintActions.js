import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { labelPrintActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.labelPrint.item,
    itemTypes.labelPrint.actionType,
    itemTypes.labelPrint.uri,
    actionTypes,
    config.appRoot
);
