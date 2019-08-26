import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { whoBuiltWhatDetailsReportActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = {};

export default reportOptionsFactory(
    reportTypes.whoBuiltWhatDetails.actionType,
    actionTypes,
    defaultState
);
