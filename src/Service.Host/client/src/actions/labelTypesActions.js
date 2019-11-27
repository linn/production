import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { labelTypesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.labelTypes.item,
    itemTypes.labelTypes.actionType,
    itemTypes.labelTypes.uri,
    actionTypes,
    config.appRoot
);
