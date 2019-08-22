import reportsResultsFactory from '../reportsResultsFactory';
import { whoBuiltWhatReportActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = { loading: false, data: null };

export default reportsResultsFactory(
    reportTypes.whoBuiltWhat.actionType,
    actionTypes,
    defaultState
);
