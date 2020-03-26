import { collectionStoreFactory } from '@linn-it/linn-form-components-library';
import { partCadInfosActionTypes as actionTypes } from '../actions';
import * as itemTypes from '../itemTypes';

const defaultState = {
    loading: false,
    items: []
};

export default collectionStoreFactory(itemTypes.partCadInfos.actionType, actionTypes, defaultState);
