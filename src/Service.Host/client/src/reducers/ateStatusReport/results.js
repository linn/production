import { reportResultsFactory } from '@linn-it/linn-form-components-library';
import { ateStatusReportActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = { loading: false, data: null };

export default reportResultsFactory(
    reportTypes.ateStatusReport.actionType,
    actionTypes,
    defaultState
);
