import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { addressActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.address.item,
    itemTypes.address.actionType,
    itemTypes.address.uri,
    actionTypes,
    config.appRoot
);
