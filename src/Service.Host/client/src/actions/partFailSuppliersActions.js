import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { partFailSuppliersActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.partFailSuppliers.item,
    itemTypes.partFailSuppliers.actionType,
    itemTypes.partFailSuppliers.uri,
    actionTypes,
    config.appRoot
);
