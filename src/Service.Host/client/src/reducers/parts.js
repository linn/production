import { collectionStoreFactory } from '@linn-it/linn-form-components-library';
import { partsActionsTypes as actionTypes } from '../actions';
import * as itemTypes from '../itemTypes';

const defaultState = {
    loading: false,
    items: [],
    searchItems: []
};

export default collectionStoreFactory(itemTypes.parts.actionType, actionTypes, defaultState);
