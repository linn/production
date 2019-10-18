import { ReportActions } from '@linn-it/linn-form-components-library';
import { manufacturingCommitDateActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.manufacturingCommitDate.item,
    reportTypes.manufacturingCommitDate.actionType,
    reportTypes.manufacturingCommitDate.uri,
    actionTypes,
    config.appRoot
);
