import { ReportActions } from '@linn-it/linn-form-components-library';
import { productionMeasuresCitsActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.productionMeasuresCitsReport,
    reportTypes.productionMeasuresCitsReport.actionType,
    reportTypes.productionMeasuresCitsReport.uri,
    actionTypes,
    config.appRoot
);
