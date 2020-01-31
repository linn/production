import { itemStoreFactory } from '@linn-it/linn-form-components-library';
import { addressesActionTypes as actionTypes } from '../actions';
import * as itemTypes from '../itemTypes';

const defaultState = {
    loading: false,
    item: {}
};

export default itemStoreFactory(itemTypes.address.actionType, actionTypes, defaultState);
