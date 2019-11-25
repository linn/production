import { ReportActions } from '@linn-it/linn-form-components-library';
import { wwdTriggerReportTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.wwdTriggerReport.item,
    reportTypes.wwdTriggerReport.actionType,
    reportTypes.wwdTriggerReport.uri,
    actionTypes,
    config.appRoot
);
