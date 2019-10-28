import { ReportActions } from '@linn-it/linn-form-components-library';
import { buildsDetailReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.buildsDetailReport.item,
    reportTypes.buildsDetailReport.actionType,
    reportTypes.buildsDetailReport.uri,
    actionTypes,
    config.appRoot
);
