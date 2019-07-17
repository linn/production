import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { ateFaultCodesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.ateFaultCodes.actionType,
    itemTypes.ateFaultCodes.uri,
    actionTypes,
    config.appRoot
);
