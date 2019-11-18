import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { printWorksOrderAioLabelsActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = { working: false, messageText: '', messageVisible: false };

export default processStoreFactory(
    processTypes.printWorksOrderAioLabels.actionType,
    actionTypes,
    defaultState,
    'Print Works Order AIO label request submitted successfully'
);
