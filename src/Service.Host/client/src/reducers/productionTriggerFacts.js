import { combineReducers } from 'redux';
import { reportsResultsFactory } from '@linn-it/linn-form-components-library';
import { productionTriggerFactsTypes as actionTypes } from '../actions';
import * as reportTypes from '../reportTypes';

const defaultState = { loading: false, data: null };

const results = reportsResultsFactory(
    reportTypes.productionTriggerFacts.actionType,
    actionTypes,
    defaultState
);

export default combineReducers({ results });
