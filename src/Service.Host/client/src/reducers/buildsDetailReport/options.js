import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { buildsDetailReportActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = {};

export default reportOptionsFactory(
    reportTypes.buildsDetailReport.actionType,
    actionTypes,
    defaultState
);
