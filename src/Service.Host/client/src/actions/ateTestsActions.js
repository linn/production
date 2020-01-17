import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { ateTestsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.ateTests.item,
    itemTypes.ateTests.actionType,
    itemTypes.ateTests.uri,
    actionTypes,
    config.appRoot
);
