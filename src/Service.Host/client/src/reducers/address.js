import { itemStoreFactory } from '@linn-it/linn-form-components-library';
import { addressActionTypes as actionTypes } from '../actions';
import * as itemTypes from '../itemTypes';

const defaultState = {
    loading: false,
    item: {},
    editStatus: 'view'
};

export default itemStoreFactory(itemTypes.address.actionType, actionTypes, defaultState);
