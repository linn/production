import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { assemblyFailFaultCodesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.assemblyFailFaultCodes.item,
    itemTypes.assemblyFailFaultCodes.actionType,
    itemTypes.assemblyFailFaultCodes.uri,
    actionTypes,
    config.appRoot
);
