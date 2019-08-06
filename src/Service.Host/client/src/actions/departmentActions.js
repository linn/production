import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { departmentsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.departments.actionType,
    itemTypes.departments.uri,
    actionTypes,
    config.appRoot
);
