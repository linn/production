import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { printAllLabelsForProductActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = { working: false, messageText: '', messageVisible: false };

export default processStoreFactory(
    processTypes.printAllLabelsForProduct.actionType,
    actionTypes,
    defaultState,
    'Print all labels for product request submitted successfully'
);
