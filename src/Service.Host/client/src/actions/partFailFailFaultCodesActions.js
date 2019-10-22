import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { partFailFaultCodesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.partFailFaultCodes.item,
    itemTypes.partFailFaultCodes.actionType,
    itemTypes.partFailFaultCodes.uri,
    actionTypes,
    config.appRoot
);
