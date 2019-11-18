import { combineReducers } from 'redux';
import { reportsResultsFactory } from '@linn-it/linn-form-components-library';
import { wwdTriggerReportTypes as actionTypes } from '../actions';
import * as reportTypes from '../reportTypes';

const defaultState = { loading: false, data: null };

const results = reportsResultsFactory(
    reportTypes.wwdTriggerReport.actionType,
    actionTypes,
    defaultState
);

export default combineReducers({ results });