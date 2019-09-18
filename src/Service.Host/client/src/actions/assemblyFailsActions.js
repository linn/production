import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { assemblyFailsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.assemblyFails.item,
    itemTypes.assemblyFails.actionType,
    itemTypes.assemblyFails.uri,
    actionTypes,
    config.appRoot
);
