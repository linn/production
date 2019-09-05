import { ReportActions } from '@linn-it/linn-form-components-library';
import { productionTriggersReportTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.productionTriggersReport.actionType,
    reportTypes.productionTriggersReport.uri,
    actionTypes,
    config.appRoot
);
