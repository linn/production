import { itemStoreFactory } from '@linn-it/linn-form-components-library';
import { labelReprintActionTypes as actionTypes } from '../actions';
import * as itemTypes from '../itemTypes';

const defaultState = {
    loading: false,
    item: null,
    editStatus: 'view',
    applicationState: { links: [] }
};

export default itemStoreFactory(itemTypes.labelReprint.actionType, actionTypes, defaultState);
