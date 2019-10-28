import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { partFailsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.partFails.item,
    itemTypes.partFails.actionType,
    itemTypes.partFails.uri,
    actionTypes,
    config.appRoot
);
