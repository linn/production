import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { partFailErrorTypesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.partFailErrorTypes.item,
    itemTypes.partFailErrorTypes.actionType,
    itemTypes.partFailErrorTypes.uri,
    actionTypes,
    config.appRoot
);
