import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { smtOutstandingWorkOrderPartsActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = { smtLine: 'All', parts: [] };

export default reportOptionsFactory(
    reportTypes.smtOutstandingWorkOrderParts.actionType,
    actionTypes,
    defaultState
);
