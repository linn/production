import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { boardFailTypesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.boardFailTypes.item,
    itemTypes.boardFailTypes.actionType,
    itemTypes.boardFailTypes.uri,
    actionTypes,
    config.appRoot
);
