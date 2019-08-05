import { collectionWithLinksStoreFactory } from '@linn-it/linn-form-components-library';
import { serialNumbersActionTypes as actionTypes } from '../actions';
import * as itemTypes from '../itemTypes';

const defaultState = {
    loading: false,
    items: [],
    links: []
};

export default collectionWithLinksStoreFactory(
    itemTypes.serialNumbers.actionType,
    actionTypes,
    defaultState
);
